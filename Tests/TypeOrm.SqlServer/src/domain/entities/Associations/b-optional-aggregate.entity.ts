import { B_OptionalDependent } from './b-optional-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn } from 'typeorm';

@Entity('b_optional_aggregate')
export class B_OptionalAggregate {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  optionalAggrAttr: string;

  @OneToOne(() => B_OptionalDependent, { cascade: ['insert', 'update'] })
  @JoinColumn()
  b_OptionalDependent?: B_OptionalDependent;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}