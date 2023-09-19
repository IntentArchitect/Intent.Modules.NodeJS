import { J_RequiredDependent } from './j-required-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('j_multiple_aggregate')
export class J_MultipleAggregate {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  multipleAggrAttr: string;

  @ManyToOne(() => J_RequiredDependent, { cascade: ['insert', 'update'], nullable: false })
  j_RequiredDependent: J_RequiredDependent;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}