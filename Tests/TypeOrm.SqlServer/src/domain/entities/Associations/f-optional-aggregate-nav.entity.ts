import { F_OptionalDependent } from './f-optional-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn } from 'typeorm';

@Entity('f_optional_aggregate_nav')
export class F_OptionalAggregateNav {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  optionalAggrNavAttr: string;

  @OneToOne(() => F_OptionalDependent, f_OptionalDependent => f_OptionalDependent.f_OptionalAggregateNav, { cascade: ['insert', 'update'] })
  @JoinColumn()
  f_OptionalDependent?: F_OptionalDependent;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}