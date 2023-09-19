import { H_MultipleDependent } from './h-multiple-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('h_optional_aggregate_nav')
export class H_OptionalAggregateNav {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  optionalAggrNavAttr: string;

  @OneToMany(() => H_MultipleDependent, (h_MultipleDependents) => h_MultipleDependents.h_OptionalAggregateNav, { cascade: ['insert', 'update'] })
  h_MultipleDependents: H_MultipleDependent[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}